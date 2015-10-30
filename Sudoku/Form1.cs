using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sudoku.Data;

namespace Sudoku
{
	public partial class Form1 : Form
	{
		Board board;

		public Form1()
		{
			InitializeComponent();

			board = new Board(Board.BoardSize.s9);
			board.SetNumber(1, 2, 5);

			System.Diagnostics.Debug.WriteLine(board);

		}
	}
}
