using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hakoiri
{
	class Program
	{
		static Queue<Board> BoardQueue = new Queue<Board>();
		static HashSet<Board> Checked = new HashSet<Board>();

		[STAThreadAttribute]
		static void Main(string[] args)
		{
			Board board = new Board();
			BoardQueue.Enqueue(board);
			Checked.Add(board);

			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			int depth = -1, c = 0;
			while (BoardQueue.Count > 0) {
				board = BoardQueue.Dequeue();

				// 進行状況表示
				if (depth != board.MoveCount) {
					depth = board.MoveCount;
					c = 0;
					Console.Write("\r\n{0},{1},{2}:", depth, BoardQueue.Count, Checked.Count);
				} else if (c % 10 == 0) {
					Console.Write(".");
				}
				c++;

				// 探索
				if (NextBoard(board)) {
					Console.WriteLine();
					// 結果表示
					sw.Stop();
					ConsoleOutput(string.Format("探索時間:{0}\r\n", sw.Elapsed));
					ConsoleOutput(string.Format("最短手順:{0}\r\n", board.MoveCount));

					List<Board> tmp = new List<Board>();
					while (board.PreviousBoard != null) {
						tmp.Add(board);
						board = board.PreviousBoard;
					}
					tmp.Add(board);
					tmp.Reverse();

					foreach (var tmpBoard in tmp) {
						if (tmpBoard.PreviousMove == null) {
							ConsoleOutput(string.Format("{0}:\r\n", tmpBoard.MoveCount));
						} else {
							int index = tmpBoard.PreviousMove.Number;
							int x = tmpBoard.Pieces[index].Position.X;
							int y = tmpBoard.Pieces[index].Position.Y;
							char mark = Board.GetMark(index);
							ConsoleOutput(string.Format("{0}:{1}({2},{3}),{4},{5}\r\n", tmpBoard.MoveCount, mark, x, y, tmpBoard.PreviousMove.Direction, tmpBoard.PreviousMove.Distance));
						}
						ConsoleOutput(string.Format("{0}\r\n", tmpBoard.ToString()));
					}
					break;
				}
			}

			System.Diagnostics.Debug.Write(buffer.ToString());
			Console.Write(buffer.ToString());

			Console.WriteLine("Hit Any Key");
			Console.ReadLine();

			Clipboard.SetDataObject(buffer.ToString(), true);
		}

		static bool NextBoard(Board board)
		{
			// 終了判定
			if (board.CheckEndPattern()) {
				return true;
			} else {
				for (int i = 0; i < board.Pieces.Count(); i++) {
					foreach (Direction d in new Direction[] { Direction.UP, Direction.DOWN, Direction.RIGHT, Direction.LEFT }) {
						Board[] nextList = board.Move(i, d);
						foreach (Board next in nextList) {
							if (next != null && !Checked.Contains(next)) {
								BoardQueue.Enqueue(next);
								Checked.Add(next);
							}
						}
					}
				}
			}
			return false;
		}

		static StringBuilder buffer = new StringBuilder();
		static void ConsoleOutput(string str)
		{
			buffer.Append(str);
			//System.Diagnostics.Debug.Write(str);
			//Console.Write(str);
		}
	}
}
