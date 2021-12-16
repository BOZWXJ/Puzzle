using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace PuzzleSolver.Models
{
	internal class Slide
	{
		static int height;
		static int width;
		static char[,] start;
		static char[,] goal;
		static readonly Dictionary<char, Piece> pieces = new();
		class Piece
		{
			public char name;
			public char same;
			public (int row, int col)[] offset;
			public (int row, int col) location;
			public bool check;
			public Piece(char n, int r, int c)
			{
				name = n;
				same = n;
				offset = new (int row, int col)[] { (0, 0) };
				location = (r, c);
			}
			public void AddOffset(int row, int col)
			{
				offset = offset.Concat(new (int, int)[] { (row - location.row, col - location.col) }).ToArray();
			}
			public bool ShapeCheck(Piece piece)
			{
				return offset.SequenceEqual(piece.offset);
			}
			public override string ToString()
			{
				return $"[{name}={same}:{string.Join(",", offset)}]";
			}
		}
		static readonly Dictionary<int, (int parent, char[,] board, string msg)> stateDict = new();
		static readonly HashSet<string> stateHash = new();

		public static string Solver(ref string ProblemText, CancellationToken token)
		{
			if (string.IsNullOrWhiteSpace(ProblemText)) {
				ProblemText = "#######\n#aabcd#\n#ee   #\n#ee   #\n#iijkl#\n#######\n#######\n#     #\n#   ee#\n#   ee#\n#     #\n#######\n";
				ProblemText = "aabcd\nee   \nee   \niijkl\n\n     \n   ee\n   ee\n     \n";
				ProblemText = "#######\n#aabcd#\n#eefg #\n#eefh #\n#iijkl#\n#######\n#######\n#     #\n#   ee#\n#   ee#\n#     #\n#######\n";
				ProblemText = "aabcd\neefg \neefh \niijkl\n\n     \n   ee\n   ee\n     \n";
				ProblemText = "#######\n#aabcd#\n#eefg #\n#eefh #\n#iijkl#\n#######\n#######\n#     #\n#   ee#\n#   ee#\n#     #\n#######\n";
				ProblemText = "BAAD\nBAAD\nCFFE\nCHIE\nG  J\n\n    \n    \n    \n AA \n AA \n";
				ProblemText = "######\n#BAAD#\n#BAAD#\n#CFFE#\n#CHIE#\n#G  J#\n######\n\n######\n#    #\n#    #\n#    #\n# AA #\n# AA #\n######\n";
			}

			// 問題文
			try {
				string[] lines = ProblemText.Split("\n", StringSplitOptions.RemoveEmptyEntries);
				if (lines.Length % 2 != 0) {
					return "Error";
				}
				height = lines.Length / 2;
				width = lines.Select(p => p.Length).Max();
				start = SetBoard(lines.Take(height));
				goal = SetBoard(lines.Skip(height));
				pieces.Clear();
				CheckPieceShape();
			} catch {
				return "Error";
			}
			// 探索
			stateDict.Clear();
			stateHash.Clear();
			int ans = Search(token);
			// 結果
			string AnswerText = "";
			if (ans > 0) {
				List<int> ansList = new();
				ansList.Add(ans);
				while (true) {
					if (0 != stateDict[ans].parent) {
						ansList.Add(stateDict[ans].parent);
						ans = stateDict[ans].parent;
					} else {
						break;
					}
				}
				ansList.Reverse();
				System.Diagnostics.Debug.WriteLine($"{ansList.Count}:{string.Join(",", ansList)}");
				AnswerText = $"{ansList.Count}\n";
				foreach (var i in ansList) {
					AnswerText += $"{stateDict[i].msg}\n{PrintBoard(stateDict[i].board)}";
				}
			} else {
				AnswerText = "No";
			}
			return AnswerText;
		}

		// 文字列を読み込む
		private static char[,] SetBoard(IEnumerable<string> collection)
		{
			char[,] result = new char[height, width];
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					result[i, j] = '#';
				}
			}
			foreach (var (s, i) in collection.Select((p, i) => (p, i))) {
				foreach (var (c, j) in s.Select((p, i) => (p, i))) {
					result[i, j] = c;
				}
			}
			return result;
		}

		// 同じ形の駒
		private static void CheckPieceShape()
		{
			// goal に含まれる、同一形状でも区別する
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					if (goal[i, j] != ' ' && goal[i, j] != '#') {
						if (!pieces.ContainsKey(goal[i, j])) {
							// 無し
							pieces.Add(goal[i, j], new Piece(goal[i, j], i, j));
						} else {
							// 有り
							pieces[goal[i, j]].AddOffset(i, j);
						}
					}
				}
			}
			// start に含まれる、同一形状は区別しない
			Dictionary<char, Piece> shape = new();
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					if (start[i, j] != ' ' && start[i, j] != '#' && !pieces.ContainsKey(start[i, j])) {
						if (!shape.TryAdd(start[i, j], new Piece(start[i, j], i, j))) {
							shape[start[i, j]].AddOffset(i, j);
						}
					}
				}
			}
			foreach (var (key, piece) in shape) {
				foreach (var (k, p) in shape) {
					if (key == k) {
						break;
					} else if (piece.ShapeCheck(p)) {
						piece.same = k;
						break;
					}
				}
				pieces.Add(key, piece);
			}
		}

		enum Direction { Up, Down, Right, Left }

		// 探索
		static int Search(CancellationToken token)
		{
			Queue<(int id, int lv)> queue = new();
			queue.Enqueue((0, 0));
			stateDict.Add(0, (0, start, ""));
			stateHash.Add(MakeHashString(start));
			while (queue.Count > 0) {
				token.ThrowIfCancellationRequested();
				var (id, lv) = queue.Dequeue();
				// 完了判定
				if (CheckComplete(stateDict[id].board)) {
					return id;
				}
				// 次
				foreach (var piece in pieces.Values) {
					piece.check = false;
				}
				for (int i = 0; i < height; i++) {
					for (int j = 0; j < width; j++) {
						if ((stateDict[id].board[i, j] != ' ' && stateDict[id].board[i, j] != '#') && !pieces[stateDict[id].board[i, j]].check) {
							pieces[stateDict[id].board[i, j]].location = (i, j);
							pieces[stateDict[id].board[i, j]].check = true;
						}
					}
				}
				foreach (var piece in pieces.Values) {
					int result = Saiki(queue, id, lv + 1, piece, Direction.Up, token);
					if (result >= 0) {
						return result;
					}
				}
			}
			return -1;
		}

		private static int Saiki(Queue<(int id, int lv)> queue, int id, int lv, Piece piece, Direction dir, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			if (CheckComplete(stateDict[id].board)) {
				return id;
			}
			var loc = piece.location;
			foreach (var d in Enum.GetValues(typeof(Direction)).Cast<Direction>().OrderBy(p => p, new DirectionComparer() { first = dir })) {
				piece.location = loc;
				var next = MakeNextState(stateDict[id].board, piece, d);
				if (next != null) {
					string nextHash = MakeHashString(next);
					if (!stateHash.Contains(nextHash)) {
						int nextId = stateDict.Count;
						queue.Enqueue((nextId, lv));
						stateDict.Add(nextId, (id, next, $"{lv}:{piece.name} {d}"));
						stateHash.Add(nextHash);
						//System.Diagnostics.Debug.WriteLine($"{id}->{nextId} [label=\"{lv}:{piece.name}{piece.location} {d}\"]\n{PrintBoard(next)}");
						int result = Saiki(queue, nextId, lv, piece, d, token);
						if (result >= 0) {
							return result;
						}
					}
				}
			}
			piece.location = loc;
			return -1;
		}

		private static char[,] MakeNextState(char[,] board, Piece piece, Direction d)
		{
			char[,] result = new char[height, width];
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					result[i, j] = board[i, j];
				}
			}
			var loc = piece.location;
			foreach (var offset in piece.offset) {
				int i = loc.row + offset.row;
				int j = loc.col + offset.col;
				result[i, j] = ' ';
			}
			piece.check = false;
			foreach (var offset in piece.offset) {
				int r = loc.row + offset.row + (d == Direction.Up ? -1 : d == Direction.Down ? 1 : 0);
				int c = loc.col + offset.col + (d == Direction.Left ? -1 : d == Direction.Right ? 1 : 0);
				if (0 <= r && r < height && 0 <= c && c < width && (board[r, c] == ' ' || board[r, c] == piece.name)) {
					result[r, c] = piece.name;
					if (!piece.check) {
						piece.location = (r, c);
					}
					piece.check = true;
				} else {
					return null;
				}
			}
			return result;
		}

		private static string MakeHashString(char[,] state)
		{
			sb.Clear();
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					if (pieces.ContainsKey(state[i, j])) {
						sb.Append(pieces[state[i, j]].same);
					} else {
						sb.Append(state[i, j]);
					}
				}
			}
			return sb.ToString();
		}

		// 同じ方向を優先
		class DirectionComparer : IComparer<Direction>
		{
			public Direction first { get; init; }
			public int Compare(Direction x, Direction y)
			{
				if (x == first) {
					return -1;
				} else if (y == first) {
					return 1;
				} else {
					return x - y;
				}
			}
		}

		// 完了
		private static bool CheckComplete(char[,] state)
		{
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					if (goal[i, j] != ' ' && goal[i, j] != state[i, j]) {
						return false;
					}
				}
			}
			return true;
		}

		static StringBuilder sb = new();
		private static string PrintBoard(char[,] data)
		{
			sb.Clear();
			for (int i = 0; i < data.GetLength(0); i++) {
				sb.Append("  ");
				for (int j = 0; j < data.GetLength(1); j++) {
					sb.Append(data[i, j]);
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

	}
}
