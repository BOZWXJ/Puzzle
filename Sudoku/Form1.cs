using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sudoku.Data;
using Sudoku.Lib;

namespace Sudoku
{
	public partial class Form1 : Form
	{
		Board board;

		public Form1()
		{
			InitializeComponent();

			board = new Board(Board.BoardSize.s9);
			//board = new Board(Board.BoardSize.s16);
			//board = new Board(Board.BoardSize.s25);
			board.SetNumber(2, 5, 8);
			board.SetGroup(2, 5, 0);
			System.Diagnostics.Debug.WriteLine(board);
			System.Diagnostics.Debug.WriteLine(board.FlagToString());
			System.Diagnostics.Debug.WriteLine(board.GroupListToString());

		}
	}
}
