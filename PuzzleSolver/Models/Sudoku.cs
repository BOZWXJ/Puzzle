using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace PuzzleSolver.Models
{
	internal class Sudoku
	{
		static int size;
		static int[,] board;
		static int[,] block;
		static (int r, int c)[][] rowList;
		static (int r, int c)[][] columnList;
		static (int r, int c)[][] blockList;
		static bool[,,] flg;

		public static string Solver(ref string problem, CancellationToken token)
		{
			if (string.IsNullOrWhiteSpace(problem)) {
				problem = "917254000\r\n402080000\r\n650003400\r\n003090256\r\n500700309\r\n200005071\r\n020530760\r\n370160098\r\n000000030\r\n"; // easy
				problem = "0 6 0 4 5 0 3 0 0\r\n0 0 0 7 0 6 4 0 0\r\n0 7 0 0 0 0 0 0 0\r\n0 0 5 0 0 0 0 8 0 \r\n0 4 0 0 1 3 0 0 0 \r\n0 2 8 0 0 0 0 1 0 \r\n8 0 0 3 6 9 0 0 5 \r\n0 0 0 0 0 0 0 0 2 \r\n0 0 0 0 4 0 0 0 0 \r\n";
				problem = "0,6,0,4,5,0,3,0,0\r\n,,,7,0,6,4,0,0\r\n0,7,0,0,0,0,0,0,0\r\n0,0,5,0,0,0,0,8,0,\r\n0,4,0,0,1,3,0,0,0,\r\n0,2,8,0,0,0,0,1,0,\r\n8,0,0,3,6,9,0,0,5,\r\n0,0,0,0,0,0,0,0,2,\r\n0,0,0,0,4,0,0,0,0,\r\n";
				problem = "060450300\r\n000706400\r\n070000000\r\n005000080\r\n040013000\r\n028000010\r\n800369005\r\n000000002\r\n000040000\r\n";
				problem = "0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n\r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n\r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n\r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n\r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \r\n\r\n";
				problem = "800000000\r\n003600000\r\n070090200\r\n050007000\r\n000045700\r\n000100030\r\n001000068\r\n008500010\r\n090000400\r\n"; // Very hard
			}

			// 問題文
			try {
				string[] lines = problem.Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
				size = lines.Length;
				if (size != 9 && size != 16 && size != 25) {
					return "Error";
				}
				string[][] cells = new string[size][];
				if (size == 9 && lines[0].Length == 9) {
					// 9x9
					foreach (var (str, i) in lines.Select((str, i) => (str, i))) {
						cells[i] = str.Select(p => p.ToString()).ToArray();
					}
				} else if (lines[0].Any(p => (p < '0' || '9' < p) && p != ' ')) {
					// 数字空白以外の文字が含まれる
					foreach (var (str, i) in lines.Select((str, i) => (str, i))) {
						cells[i] = new string[size];
						int j = 0;
						foreach (var c in str) {
							if (c == ' ') {
							} else if ('0' <= c && c <= '9') {
								cells[i][j] += c.ToString();
							} else {
								j++;
							}
						}
					}
				} else {
					// 空白区切り
					foreach (var (str, i) in lines.Select((str, i) => (str, i))) {
						cells[i] = str.Split((char[])null, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
					}
				}
				board = new int[size, size];
				for (int i = 0; i < size; i++) {
					for (int j = 0; j < size; j++) {
						int num;
						if (int.TryParse(cells[i][j], out num)) {
							board[i, j] = num;
						}
					}
				}

				// todo: 不定形対応予定
				int x = size == 9 ? 3 : size == 16 ? 4 : 5;
				block = new int[size, size];
				for (int i = 0; i < size; i++) {
					for (int j = 0; j < size; j++) {
						int b = i / x * x + j / x;
						int k = j % x + i % x * x;
						block[i, j] = b;
					}
				}

				// チェック用リスト作成
				rowList = new (int r, int c)[size][];
				columnList = new (int r, int c)[size][];
				blockList = new (int r, int c)[size][];
				for (int i = 0; i < size; i++) {
					rowList[i] = new (int r, int c)[size];
					columnList[i] = new (int r, int c)[size];
					blockList[i] = new (int r, int c)[size];
				}
				int[] index = new int[size];
				for (int i = 0; i < size; i++) {
					for (int j = 0; j < size; j++) {
						rowList[i][j] = (i, j);
						columnList[i][j] = (j, i);
						int b = block[i, j];
						int k = index[b];
						blockList[b][k] = (i, j);
						index[b]++;
					}
				}
				// 
				flg = new bool[size, size, size + 1];
				UpdateAllFlg();
			} catch {
				return "Error";
			}

			// 探索
			while (SearchNumber(out int row, out int col, out int num)) {
				SetNumber(row, col, num);
			}
			int d = 0;
			bool result = Saiki(ref d, token);

			// 結果
			string answer = PrintBoard();
			if (!result) {
				answer += PrintFlg();
			}
			return answer;
		}

		static bool Saiki(ref int d, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			int id = d++;
			// 完了判定 空きマス検索
			if (CheckComplete(out int row, out int col)) {
				return true;
			}
			// 状態の保存
			int[,] boardBackup = new int[size, size];
			Array.Copy(board, boardBackup, board.Length);
			// 空きマスに入る数字
			for (int num = 1; num <= size; num++) {
				if (!flg[row, col, num]) {
					// 仮に入れる
					SetNumber(row, col, num);
					// 決定できる所を決める
					while (SearchNumber(out int r, out int c, out int n)) {
						SetNumber(r, c, n);
					}
					// 完了判定
					if (CheckComplete(out _, out _)) {
						return true;
					}
					// 矛盾判定
					if (!CheckError()) {
						// 再帰
						System.Diagnostics.Debug.WriteLine($"{id}->{d} [label=\"{row},{col}={num}\"]");
						if (Saiki(ref d, token)) {
							return true;
						}
					}
					// 状態の復元
					Array.Copy(boardBackup, board, board.Length);
					UpdateAllFlg();
				}
			}
			return false;
		}

		static bool CheckComplete(out int row, out int col)
		{
			for (int i = 0; i < size; i++) {
				for (int j = 0; j < size; j++) {
					if (board[i, j] == 0) {
						row = i;
						col = j;
						return false;
					}
				}
			}
			row = 0;
			col = 0;
			return true;
		}

		static bool CheckError()
		{
			// cell
			for (int i = 0; i < size; i++) {
				for (int j = 0; j < size; j++) {
					if (board[i, j] == 0) {
						int cnt = 0;
						for (int n = 1; n <= size; n++) {
							if (!flg[i, j, n]) {
								cnt++;
							}
						}
						if (cnt == 0) {
							return true;
						}
					}
				}
			}
			// row
			if (CheckError(rowList)) {
				return true;
			}
			// col
			if (CheckError(columnList)) {
				return true;
			}
			// block
			if (CheckError(blockList)) {
				return true;
			}
			return false;
		}
		static bool CheckError((int r, int c)[][] list)
		{
			for (int n = 1; n <= size; n++) {
				for (int i = 0; i < size; i++) {
					int cnt = 0;
					for (int j = 0; j < size; j++) {
						if (board[list[i][j].r, list[i][j].c] == n || !flg[list[i][j].r, list[i][j].c, n]) {
							cnt++;
						}
					}
					if (cnt == 0) {
						return true;
					}
				}
			}
			return false;
		}

		static bool SearchNumber(out int row, out int col, out int num)
		{
			num = 0;
			// cell
			for (row = 0; row < size; row++) {
				for (col = 0; col < size; col++) {
					int cnt = 0;
					for (int n = 1; n <= size; n++) {
						if (!flg[row, col, n]) {
							num = n;
							cnt++;
						}
					}
					if (cnt == 1 && board[row, col] == 0) {
						return true;
					}
				}
			}
			// row
			if (SearchNumber(rowList, out row, out col, out num)) {
				return true;
			}
			// col
			if (SearchNumber(columnList, out row, out col, out num)) {
				return true;
			}
			// block
			if (SearchNumber(blockList, out row, out col, out num)) {
				return true;
			}
			return false;
		}

		static bool SearchNumber((int r, int c)[][] list, out int row, out int col, out int num)
		{
			num = 0;
			row = 0;
			col = 0;
			for (int n = 1; n <= size; n++) {
				for (int i = 0; i < size; i++) {
					int cnt = 0;
					for (int j = 0; j < size; j++) {
						if (!flg[list[i][j].r, list[i][j].c, n]) {
							row = list[i][j].r;
							col = list[i][j].c;
							cnt++;
						}
					}
					if (cnt == 1 && board[row, col] == 0) {
						num = n;
						return true;
					}
				}
			}
			return false;
		}

		static void UpdateAllFlg()
		{
			for (int r = 0; r < size; r++) {
				for (int c = 0; c < size; c++) {
					for (int n = 1; n <= size; n++) {
						flg[r, c, n] = false;
					}
				}
			}
			for (int r = 0; r < size; r++) {
				for (int c = 0; c < size; c++) {
					if (board[r, c] != 0) {
						for (int i = 1; i <= size; i++) {
							flg[r, c, i] = true;
						}
						SetFlg(board[r, c], rowList[r]);
						SetFlg(board[r, c], columnList[c]);
						SetFlg(board[r, c], blockList[block[r, c]]);
					}
				}
			}
		}

		static void SetNumber(int row, int col, int num)
		{
			board[row, col] = num;
			for (int i = 1; i <= size; i++) {
				flg[row, col, i] = true;
			}
			SetFlg(num, rowList[row]);
			SetFlg(num, columnList[col]);
			SetFlg(num, blockList[block[row, col]]);
		}

		static void SetFlg(int n, (int r, int c)[] list)
		{
			foreach (var (r, c) in list) {
				flg[r, c, n] = true;
			}
		}

		static StringBuilder sb = new();
		private static string PrintBoard()
		{
			sb.Clear();
			for (int i = 0; i < size; i++) {
				for (int j = 0; j < size; j++) {
					if (j > 0) {
						if (size != 9) {
							sb.Append(" ");
						}
					}
					sb.Append(board[i, j].ToString().PadLeft(size == 9 ? 1 : 2));
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

		private static string PrintFlg()
		{
			sb.Clear();
			int x = size == 9 ? 3 : size == 16 ? 4 : 5;
			string s1 = new('#', (x + 1) * size + 1);
			string s2 = "+";
			for (int i = 0; i < size; i++) {
				s2 += $"{new('-', x)}+";
			}
			for (int r = 0; r < size; r++) {
				if (r == 0) {
					sb.AppendLine(s1);
				}
				for (int i = 0; i < x; i++) {
					for (int c = 0; c < size; c++) {
						if (c == 0) {
							sb.Append("#");
						}
						for (int j = 0; j < x; j++) {
							int n = i * x + j + 1;
							sb.Append(flg[r, c, n] ? new(' ', size == 9 ? 1 : 2) : n.ToString().PadLeft(size == 9 ? 1 : 2));
						}
						if (c % x == x - 1) {
							sb.Append("#");
						} else {
							sb.Append("|");
						}
					}
					sb.AppendLine();
				}
				if (r % x == x - 1) {
					sb.AppendLine(s1);
				} else {
					sb.AppendLine(s2);
				}
			}
			return sb.ToString();
		}

	}
}
