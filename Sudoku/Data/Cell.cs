using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Data
{
	class Cell
	{
		public int Number;
		public int Flag;

		public bool Freeze;

		public int ColumnIndex { get; private set; }
		public int RowIndex { get; private set; }
		public int GroupIndex { get; set; }

		public Cell(int col, int row, int group)
		{
			Number = 0;
			Flag = 0;

			Freeze = false;

			ColumnIndex = col;
			RowIndex = row;
			GroupIndex = group;
		}

		public override string ToString()
		{
			return string.Format("({0} {1:x}:x{2}, y{3}, g{4})", Number, Flag, ColumnIndex, RowIndex, GroupIndex);
		}
	}
}
