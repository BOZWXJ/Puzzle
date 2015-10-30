using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakoiri
{
	class Piece
	{
		public readonly int ShapeNo;
		public readonly Point Position;
		public Point[] Shape { get { return PieceShapes.GetShape(ShapeNo); } }

		public Piece(int shapeNo, int x, int y)
		{
			ShapeNo = shapeNo;
			Position = new Point(x, y);
		}
		public Piece(int shapeNo, Point point) : this(shapeNo, point.X, point.Y) { }
		public Piece(Piece piece) : this(piece.ShapeNo, piece.Position.X, piece.Position.Y) { }

		public static bool operator !=(Piece left, Piece right) { return !left.Equals(right); }
		public static bool operator ==(Piece left, Piece right) { return left.Equals(right); }

		public override bool Equals(object obj)
		{
			if (obj is Piece) {
				Piece other = (Piece)obj;
				return ShapeNo == other.ShapeNo && Position == other.Position;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return ShapeNo ^ Position.GetHashCode() << 8;
		}

		public override string ToString()
		{
			return string.Format("Piece:({0},{1})", ShapeNo, Position);
		}
	}

	class Point
	{
		public readonly int X;
		public readonly int Y;
		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static bool operator !=(Point left, Point right) { return !left.Equals(right); }
		public static bool operator ==(Point left, Point right) { return left.Equals(right); }

		public override bool Equals(object obj)
		{
			if (obj is Point) {
				Point p = (Point)obj;
				return X == p.X && Y == p.Y;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return X ^ Y << 8;
		}

		public override string ToString()
		{
			return string.Format("Point:({0},{1})", X, Y);
		}
	}
}
