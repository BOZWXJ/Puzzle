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
		public static string Solver(ref string ProblemText, CancellationToken token)
		{
			if (string.IsNullOrWhiteSpace(ProblemText)) {
				ProblemText = "917254000\n402080000\n650003400\n003090256\n500700309\n200005071\n020530760\n370160098\n000000030\n"; // easy
				ProblemText = "0 6 0 4 5 0 3 0 0\n0 0 0 7 0 6 4 0 0\n0 7 0 0 0 0 0 0 0\n0 0 5 0 0 0 0 8 0 \n0 4 0 0 1 3 0 0 0 \n0 2 8 0 0 0 0 1 0 \n8 0 0 3 6 9 0 0 5 \n0 0 0 0 0 0 0 0 2 \n0 0 0 0 4 0 0 0 0 \n";
				ProblemText = "0,6,0,4,5,0,3,0,0\n,,,7,0,6,4,0,0\n0,7,0,0,0,0,0,0,0\n0,0,5,0,0,0,0,8,0,\n0,4,0,0,1,3,0,0,0,\n0,2,8,0,0,0,0,1,0,\n8,0,0,3,6,9,0,0,5,\n0,0,0,0,0,0,0,0,2,\n0,0,0,0,4,0,0,0,0,\n";
				ProblemText = "060450300\n000706400\n070000000\n005000080\n040013000\n028000010\n800369005\n000000002\n000040000\n"; // hard
				ProblemText = "0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n\n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  \n\n";
				ProblemText = "800000000\n003600000\n070090200\n050007000\n000045700\n000100030\n001000068\n008500010\n090000400\n"; // Very hard
			}

			// 問題文
			try {
				string[] lines = ProblemText.Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
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
			string AnswerText = PrintBoard();
			if (!result) {
				AnswerText += PrintFlg();
			}
			return AnswerText;
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
