using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Data
{
	class Board
	{
		public enum BoardSize { s9 = 3, s16 = 4, s25 = 5 }

		int digits;
		int unitSize;
		int boardSize;
		int[] data;

		public Board(BoardSize n)
		{
			digits = n == BoardSize.s9 ? 1 : 2;
			unitSize = (int)n;
			boardSize = unitSize * unitSize;
			data = new int[boardSize * boardSize];
		}

		public bool SetNumber(int num, int x, int y)
		{
			int index = x + y * boardSize;
			data[index] = num;


			return true;
		}

		StringBuilder sb = new StringBuilder();
		public override string ToString()
		{
			sb.Length = 0;
			for (int y = 0; y < boardSize; y++) {
				if (y == 0) {
					sb.Append("".PadLeft(digits + 1));
					for (int x = 0; x < boardSize; x++) {
						if (x % unitSize == 0) {
							sb.Append(" ");
						}
						sb.AppendFormat(string.Format("{{0,{0}}}", digits + 1), x);
					}
					sb.AppendLine();
				}
				if (y % unitSize == 0) {
					sb.Append("+".PadLeft(digits + 2));
					for (int i = 0; i < unitSize; i++) {
						sb.Append("".PadLeft(unitSize * (digits + 1), '-'));
						if (i < unitSize - 1) {
							sb.Append("+");
						}
					}
					sb.AppendLine("+");
				}

				for (int x = 0; x < boardSize; x++) {
					if (x == 0) {
						sb.AppendFormat(string.Format("{{0,{0}}}|", digits + 1), y);
					} else if (x % unitSize == 0) {
						sb.Append("|");
					}
					sb.AppendFormat(string.Format("{{0,{0}}}", digits + 1), data[x + y * boardSize]);
				}
				sb.AppendLine("|");
			}

			sb.Append("+".PadLeft(digits + 2));
			for (int i = 0; i < unitSize; i++) {
				sb.Append("".PadLeft(unitSize * (digits + 1), '-'));
				if (i < unitSize - 1) {
					sb.Append("+");
				}
			}
			sb.AppendLine("+");

			return sb.ToString();
		}

	}
}
