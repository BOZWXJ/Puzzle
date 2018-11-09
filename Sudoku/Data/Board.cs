using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Data
{
	class Board
	{
		public enum BoardSize { s9 = 3, s16 = 4, s25 = 5 }

		int unitSize;
		int boardSize;

		Cell[] data;
		List<List<Cell>> GroupList = new List<List<Cell>>();

		int dataDigits;
		int flagDigits;
		int flagDefault;

		public Board(BoardSize n)
		{
			switch (n) {
			case BoardSize.s9:
				unitSize = 3;
				boardSize = 9;
				dataDigits = 1;
				flagDigits = 3;
				flagDefault = 0x000001ff;
				break;
			case BoardSize.s16:
				unitSize = 4;
				boardSize = 16;
				dataDigits = 2;
				flagDigits = 4;
				flagDefault = 0x0000ffff;
				break;
			case BoardSize.s25:
				unitSize = 5;
				boardSize = 25;
				dataDigits = 2;
				flagDigits = 7;
				flagDefault = 0x01ffffff;
				break;
			default:
				throw (new ArgumentException());
			}

			for (int i = 0; i < boardSize * 3; i++) {
				GroupList.Add(new List<Cell>());
			}

			data = new Cell[boardSize * boardSize];
			for (int row = 0; row < boardSize; row++) {
				for (int col = 0; col < boardSize; col++) {
					int i = col + row * boardSize;
					int group = col / unitSize + (row / unitSize) * unitSize;
					data[i] = new Cell(col, row, group);
					data[i].Flag = flagDefault;
					GroupList[col].Add(data[i]);
					GroupList[row + boardSize].Add(data[i]);
					GroupList[group + boardSize * 2].Add(data[i]);
				}
			}
		}

		#region 数値の操作

		/// <summary>
		/// セルの数値を返す
		/// </summary>
		/// <param name="col">列位置(X)</param>
		/// <param name="row">行位置(Y)</param>
		/// <returns>数値</returns>
		public int GetNumber(int col, int row)
		{
			return data[col + row * boardSize].Number;
		}

		/// <summary>
		/// セルに数値をセットする
		/// </summary>
		/// <param name="col">列位置(X)</param>
		/// <param name="row">行位置(Y)</param>
		/// <param name="num">数値</param>
		public void SetNumber(int col, int row, int num)
		{
			data[col + row * boardSize].Number = num;
		}

		#endregion

		// 固定

		// フラグの操作

		public bool GetFlag(int col, int row, int num)
		{
			return (data[col + row * boardSize].Flag & (1 << num - 1)) != 0;
		}

		public int GetFlags(int col, int row)
		{
			return data[col + row * boardSize].Flag;
		}

		public void SetFlag(int col, int row, int num, bool val)
		{
			if (val) {
				data[col + row * boardSize].Flag |= (1 << num - 1);
			} else {
				data[col + row * boardSize].Flag &= ~(1 << num - 1);
			}
		}

		public void ResetFlags(int col, int row)
		{
			data[col + row * boardSize].Flag = flagDefault;
		}

		// 自動計算

		#region グループの操作

		/// <summary>
		/// セルのグループを返す
		/// </summary>
		/// <param name="col">列位置(X)</param>
		/// <param name="row">行位置(Y)</param>
		/// <returns>グループ番号</returns>
		public int GetGroup(int col, int row)
		{
			return data[col + row * boardSize].GroupIndex;
		}

		/// <summary>
		/// セルのグループをセットする
		/// </summary>
		/// <param name="col">列位置(X)</param>
		/// <param name="row">行位置(Y)</param>
		/// <param name="group">グループ番号</param>
		public void SetGroup(int col, int row, int group)
		{
			data[col + row * boardSize].GroupIndex = group;

			refreshGroupList();
		}

		/// <summary>
		/// グループリストの更新
		/// </summary>
		private void refreshGroupList()
		{
			for (int group = 0; group < boardSize; group++) {
				GroupList[group + boardSize * 2].Clear();
			}
			foreach (var c in data) {
				GroupList[c.GroupIndex + boardSize * 2].Add(c);
			}
		}

		/// <summary>
		/// グループ分けが正しいかチェック
		/// </summary>
		/// <returns></returns>
		public bool CheckGroup()
		{
			return GroupList.All(p => p.Count == boardSize);
		}

		#endregion

		#region ToString

		StringBuilder sb = new StringBuilder();
		public override string ToString()
		{
			sb.Length = 0;
			for (int y = 0; y < boardSize; y++) {
				if (y == 0) {
					sb.Append("".PadLeft(dataDigits));
					for (int x = 0; x < boardSize; x++) {
						if (x % unitSize == 0) {
							sb.Append(" ");
						}
						sb.AppendFormat(string.Format("{{0,{0}}}", dataDigits + 1), x);
					}
					sb.AppendLine();
				}
				if (y % unitSize == 0) {
					sb.Append("+".PadLeft(dataDigits + 1));
					for (int i = 0; i < unitSize; i++) {
						sb.Append("".PadLeft(unitSize * (dataDigits + 1), '-'));
						if (i < unitSize - 1) {
							sb.Append("+");
						}
					}
					sb.AppendLine("+");
				}

				for (int x = 0; x < boardSize; x++) {
					if (x == 0) {
						sb.AppendFormat(string.Format("{{0,{0}}}|", dataDigits), y);
					} else if (x % unitSize == 0) {
						sb.Append("|");
					}
					sb.AppendFormat(string.Format("{{0,{0}}}", dataDigits + 1), data[x + y * boardSize].Number);
				}
				sb.AppendLine("|");
			}

			sb.Append("+".PadLeft(dataDigits + 1));
			for (int i = 0; i < unitSize; i++) {
				sb.Append("".PadLeft(unitSize * (dataDigits + 1), '-'));
				if (i < unitSize - 1) {
					sb.Append("+");
				}
			}
			sb.AppendLine("+");

			return sb.ToString();
		}

		public string FlagToString()
		{
			sb.Length = 0;
			for (int y = 0; y < boardSize; y++) {
				if (y == 0) {
					sb.Append("".PadLeft(dataDigits));
					for (int x = 0; x < boardSize; x++) {
						if (x % unitSize == 0) {
							sb.Append(" ");
						}
						sb.AppendFormat(string.Format("{{0,{0}}}", flagDigits + 1), x);
					}
					sb.AppendLine();
				}
				if (y % unitSize == 0) {
					sb.Append("+".PadLeft(dataDigits + 1));
					for (int i = 0; i < unitSize; i++) {
						sb.Append("".PadLeft(unitSize * (flagDigits + 1), '-'));
						if (i < unitSize - 1) {
							sb.Append("+");
						}
					}
					sb.AppendLine("+");
				}

				for (int x = 0; x < boardSize; x++) {
					if (x == 0) {
						sb.AppendFormat(string.Format("{{0,{0}}}|", dataDigits), y);
					} else if (x % unitSize == 0) {
						sb.Append("|");
					}
					string fStr = string.Format("{{0,{0}:x}}", flagDigits + 1);

					sb.AppendFormat(fStr, data[x + y * boardSize].Flag);
				}
				sb.AppendLine("|");
			}

			sb.Append("+".PadLeft(dataDigits + 1));
			for (int i = 0; i < unitSize; i++) {
				sb.Append("".PadLeft(unitSize * (flagDigits + 1), '-'));
				if (i < unitSize - 1) {
					sb.Append("+");
				}
			}
			sb.AppendLine("+");

			return sb.ToString();
		}

		public string GroupListToString()
		{
			sb.Length = 0;
			foreach (var item in GroupList.Select((Value, Index) => new { Value, Index })) {
				sb.Append(item.Index);
				sb.Append(":");
				foreach (var c in item.Value) {
					sb.Append(c);
					sb.Append(", ");
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

		#endregion

	}
}
