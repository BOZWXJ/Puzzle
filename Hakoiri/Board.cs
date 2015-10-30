using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakoiri
{
	enum Direction { UP, DOWN, RIGHT, LEFT }

	static class PieceShapes
	{
		// 駒 形状設定
		static readonly string[][] shapes = { new string[] { "+*",		// 0
											 				 "**" },
											  new string[] { "+",		// 1
											 				 "*" },
											  new string[] { "+*" },	// 2
											  new string[] { "+", } };	// 3
		static Point[][] PointList;
		public static Point[] GetShape(int shapeNo) { return PointList[shapeNo]; }
		static PieceShapes()
		{
			PointList = new Point[shapes.Length][];
			List<Point> tmp = new List<Point>();

			for (int i = 0; i < shapes.Length; i++) {
				int ox = 0, oy = 0;
				bool f = false;
				string[] shape = shapes[i];
				// 原点
				for (int y = 0; y < shape.Length; y++) {
					for (int x = 0; x < shape[y].Length; x++) {
						if (shape[y][x] == '+') {
							ox = x;
							oy = y;
							f = true;
							break;
						}
					}
					if (f) { break; }
				}
				// 座標
				tmp.Clear();
				for (int y = 0; y < shape.Length; y++) {
					for (int x = 0; x < shape[y].Length; x++) {
						if (shape[y][x] != ' ') {
							tmp.Add(new Point(x - ox, y - oy));
						}
					}
				}
				PointList[i] = tmp.ToArray();
			}
		}
	}

	static class BoardSetting
	{
		// 盤 形状設定
		static readonly string[] StartPattern = { "******",
												  "*10 1*",
												  "*    *",
												  "*12 1*",
												  "* 33 *",
												  "*3  3*",
												  "******" };	// 90手
#if false
		static readonly string[] StartPattern = { "******",
												  "*1111*",
												  "*    *",
												  "*0 2 *",
												  "*    *",
												  "*3333*",
												  "******" };	// 10手
#endif
		static readonly string[] EndPattern = { "******",
											    "*    *",
											    "*    *",
											    "*    *",
											    "* 0  *",
											    "*    *",
											    "******" };
		public static Piece[] StartPieces { get; private set; }
		public static Piece[] EndPieces { get; private set; }
		public static int Width { get; private set; }
		public static int Height { get; private set; }
		public static char[,] GetMap()
		{
			char[,] result = new char[Map.GetLength(0), Map.GetLength(1)];
			for (int i = 0; i < Map.GetLength(0); i++) {
				for (int j = 0; j < Map.GetLength(1); j++) {
					result[i, j] = Map[i, j];
				}
			}
			return result;
		}
		static char[,] Map;
		static BoardSetting()
		{
			List<Piece> tmp = new List<Piece>();
			for (int y = 0; y < StartPattern.Length; y++) {
				for (int x = 0; x < StartPattern[y].Length; x++) {
					if (StartPattern[y][x] == '*') {
						// 壁
						// nothing
					} else if (StartPattern[y][x] != ' ') {
						// 駒
						int num = int.Parse(StartPattern[y][x].ToString());
						tmp.Add(new Piece(num, x, y));
					}
				}
				Width = Math.Max(Width, StartPattern[y].Length);
			}
			Height = StartPattern.Length;
			StartPieces = tmp.ToArray();

			tmp.Clear();
			for (int y = 0; y < EndPattern.Length; y++) {
				for (int x = 0; x < EndPattern[y].Length; x++) {
					if (EndPattern[y][x] == '*') {
						// 壁
						// nothing
					} else if (EndPattern[y][x] != ' ') {
						// 駒
						int num = int.Parse(EndPattern[y][x].ToString());
						tmp.Add(new Piece(num, x, y));
					}
				}
			}
			EndPieces = tmp.ToArray();

			Map = new char[Width, Height];
			for (int y = 0; y < Map.GetLength(1); y++) {
				for (int x = 0; x < Map.GetLength(0); x++) {
					if (y < StartPattern.Length && x < StartPattern[y].Length && StartPattern[y][x] == '*') {
						Map[x, y] = '*';
					} else {
						Map[x, y] = ' ';
					}
				}
			}
		}
	}

	class Board : IEquatable<Board>
	{
		public Piece[] Pieces { get; private set; }

		public int MoveCount { get; private set; }
		public Board PreviousBoard { get; private set; }
		public MoveData PreviousMove { get; private set; }

		public Board()
		{
			Pieces = new Piece[BoardSetting.StartPieces.Length];
			BoardSetting.StartPieces.CopyTo(Pieces, 0);
			MoveCount = 0;
			PreviousBoard = null;
			PreviousMove = null;
		}

		public Board(Board board)
		{
			Pieces = new Piece[board.Pieces.Length];
			board.Pieces.CopyTo(Pieces, 0);
			MoveCount = board.MoveCount;
			PreviousBoard = board;
			PreviousMove = null;
		}

		public bool CheckEndPattern()
		{
			foreach (Piece piece in BoardSetting.EndPieces) {
				if (!Pieces.Contains(piece)) {
					return false;
				}
			}
			return true;
		}

		public Board[] Move(int num, Direction direction)
		{
			// 移動判定用 準備
			char[,] map = BoardSetting.GetMap();
			for (int i = 0; i < Pieces.Length; i++) {
				Point pos1 = Pieces[i].Position;
				if (i != num) {
					foreach (Point offset in Pieces[i].Shape) {
						map[pos1.X + offset.X, pos1.Y + offset.Y] = '*';
					}
				}
			}
			// 方向
			int xSign = 0, ySign = 0;
			if (direction == Direction.UP) {
				ySign = -1;
			} else if (direction == Direction.DOWN) {
				ySign = 1;
			} else if (direction == Direction.RIGHT) {
				xSign = 1;
			} else if (direction == Direction.LEFT) {
				xSign = -1;
			}
			// 移動判定
			Board[] result = new Board[0];
			int count = 1;
			while (true) {
				Point pos = Pieces[num].Position;
				pos = new Point(pos.X + xSign * count, pos.Y + ySign * count);
				foreach (Point offset in Pieces[num].Shape) {
					int x = pos.X + offset.X;
					int y = pos.Y + offset.Y;
					if (x < 0 || map.GetLength(0) <= x || y < 0 || map.GetLength(1) <= y || map[x, y] != ' ') {
						return result;
					}
				}
				Board next = new Board(this);
				next.Pieces[num] = new Piece(next.Pieces[num].ShapeNo, pos);
				next.PreviousMove = new MoveData(num, direction, count);
				next.MoveCount++;

				Board[] tmp = new Board[result.Length + 1];
				result.CopyTo(tmp, 0);
				result = tmp;
				result[result.Length - 1] = next;

				count++;
			}
		}

		const string mark = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		static public char GetMark(int index)
		{
			return mark[index % mark.Length];
		}

		static readonly StringBuilder sb = new StringBuilder();
		public override string ToString()
		{
			char[,] map = BoardSetting.GetMap();
			for (int i = 0; i < Pieces.Length; i++) {
				Point pos = Pieces[i].Position;
				foreach (Point offset in Pieces[i].Shape) {
					map[pos.X + offset.X, pos.Y + offset.Y] = GetMark(i);
				}
			}

			sb.Length = 0;
			for (int y = 0; y < BoardSetting.Height; y++) {
				for (int x = 0; x < BoardSetting.Width; x++) {
					sb.Append(map[x, y]);
				}
				sb.AppendLine();
			}
			return sb.ToString().Trim();
		}

		public bool Equals(Board other)
		{
			foreach (Piece piece in other.Pieces) {
				if (!Pieces.Contains(piece)) {
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			int result = 0;
			foreach (Piece piece in Pieces) {
				result ^= piece.GetHashCode();
			}
			return result;
		}
	}

	class MoveData
	{
		public int Number { get; private set; }
		public Direction Direction { get; private set; }
		public int Distance { get; private set; }
		public MoveData(int num, Direction direction, int distance)
		{
			Number = num;
			Direction = direction;
			Distance = distance;
		}
		public override string ToString()
		{
			return string.Format("MoveData:({0},{1},{2})", Number, Direction, Distance);
		}
	}

}
