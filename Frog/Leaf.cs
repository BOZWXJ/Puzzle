using System.Collections.Generic;

namespace Frog
{
	class Leaf
	{
		public string Num;
		public int X;
		public int Y;
		public string Name { get { return string.Format("{0,2}({1},{2}) {3}", Num, X, Y, Range); } }

		/// <summary>
		/// 移動距離
		/// </summary>
		public int Range;
		/// <summary>
		/// 未通過フラグ
		/// </summary>
		public bool Cross;
		/// <summary>
		/// 行き先リスト
		/// </summary>
		public List<Leaf> NextLeaf;
		public int NextIndex;

		public Leaf(int x, int y)
		{
			Num = "";
			X = x;
			Y = y;
			Range = 0;
			Cross = false;
			NextLeaf = new List<Leaf>();
			NextIndex = 0;
		}

		public Leaf GetNextLeaf()
		{
			Leaf next;
			if (NextLeaf.Count != 0) {
				// 行き先有り
				do {
					if (NextIndex >= NextLeaf.Count) {
						// 全て通過済み
						Cross = true;
						NextIndex = 0;
						return null;
					}
					next = NextLeaf[NextIndex];
					NextIndex++;
				} while (!next.Cross);
			} else {
				// 行き先無し
				// 通過フラグリセット
				Cross = true;
				return null;
			}
			return next;
		}

	}
}
