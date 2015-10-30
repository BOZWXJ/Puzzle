using System;
using System.Collections.Generic;
using System.Text;

namespace Frog
{
	class Frog
	{
		Stack<Leaf> st;
		Marsh marsh;

		int moveCount;

		public Frog(Marsh marsh)
		{
			this.marsh = marsh;

			st = new Stack<Leaf>(marsh.leafCount);
			st.Push(marsh.start);
			moveCount = 0;
		}

		public int Move()
		{
			moveCount++;
			Leaf now = st.Peek();
			Leaf next = now.GetNextLeaf();
			if (next != null) {
				next.Cross = false;
				st.Push(next);
				//System.Diagnostics.Debug.WriteLine(moveCount + ":" + now.Name + " -> " + next.Name);
				if (next == marsh.goal && marsh.isComplete()) {
					return 1;
				}
			} else {
				next = st.Pop();
				//System.Diagnostics.Debug.WriteLine(moveCount + ":" + now.Name + " back");
				if (st.Count == 0) {
					return -1;
				}
			}
			return 0;
		}
		public string GetStackString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("探索回数:{0}\r\n", moveCount);

			Leaf[] list = st.ToArray();
			Array.Reverse(list);
			int i = 0;
			foreach (Leaf leaf in list) {
				sb.AppendFormat("{0}\r\n", leaf.Name);
				leaf.Num = i.ToString();
				i++;
			}
			sb.AppendLine(marsh.GetLotusString2());

			return sb.ToString();
		}

	}
}
