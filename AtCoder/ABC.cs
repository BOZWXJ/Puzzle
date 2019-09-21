using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AtCoder
{
	// ABC138D X
	public class Program
	{
		static void Main(string[] args)
		{
			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });


			int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			int N = vs[0];
			int Q = vs[1];

			MultiMap<int, int> ki = new MultiMap<int, int>();
			for (int i = 0; i < N - 1; i++) {
				vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
				ki.Add(vs[0] - 1, vs[1] - 1);
			}

			long[] ans = new long[N];
			for (int i = 0; i < Q; i++) {
				vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
				ans[vs[0] - 1] += vs[1];
			}

			Queue<int> queue = new Queue<int>();
			queue.Enqueue(0);
			while (queue.Count > 0) {
				int no = queue.Dequeue();
				if (ki.ContainsKey(no)) {
					foreach (var i in ki[no]) {
						ans[i] += ans[no];
						queue.Enqueue(i);
					}
				}
			}
			Console.WriteLine(string.Join(" ", ans));


			Console.Out.Flush();
		}
	}
}

#region MultiMap<TKey, TValue>

namespace AtCoder
{
	public class MultiMap<TKey, TValue> : Dictionary<TKey, List<TValue>>
	{
		public void Add(TKey key, TValue value)
		{
			if (!ContainsKey(key)) {
				Add(key, new List<TValue>());
			}
			this[key].Add(value);
		}
		private new void Add(TKey key, List<TValue> values)
		{
			base.Add(key, values);
		}
	}
}

#endregion

#region PriorityQueue<T> 優先度付きキュー

namespace AtCoder
{
	public class PriorityQueue<T> where T : IComparable
	{
		private readonly List<T> _Heap;
		private readonly Comparison<T> _Compare;

		public int Count { get { return _Heap.Count; } }

		public PriorityQueue() : this(false) { }
		public PriorityQueue(bool reverse)
		{
			_Heap = new List<T>();
			if (!reverse) {
				_Compare = Comparer<T>.Default.Compare;
			} else {
				_Compare = (T x, T y) => Comparer<T>.Default.Compare(y, x);
			}
		}

		public void Enqueue(T item)
		{
			_Heap.Add(item);
			int i = _Heap.Count - 1;
			while (i > 0) {
				int p = (i - 1) / 2;
				if (_Compare(_Heap[p], item) <= 0) {
					break;
				}
				_Heap[i] = _Heap[p];
				i = p;
			}
			_Heap[i] = item;
		}

		public T Dequeue()
		{
			int size = _Heap.Count - 1;
			T ret = _Heap[0];
			T x = _Heap[size];
			int i = 0;
			while (i * 2 + 1 < size) {
				var a = i * 2 + 1;
				var b = i * 2 + 2;
				if (b < size && _Compare(_Heap[b], _Heap[a]) < 0) {
					a = b;
				}
				if (_Compare(_Heap[a], x) >= 0) {
					break;
				}
				_Heap[i] = _Heap[a];
				i = a;
			}
			_Heap[i] = x;
			_Heap.RemoveAt(size);
			return ret;
		}

		public T Peek()
		{
			return _Heap[0];
		}

		public void Clear()
		{
			_Heap.Clear();
		}

		public List<T>.Enumerator GetEnumerator()
		{
			return _Heap.GetEnumerator();
		}

		public override string ToString()
		{
			return string.Join(" ", _Heap);
		}
	}
}

#endregion

#region MultiSet<T>

namespace AtCoder
{
	public class MultiSet<T>
	{
		private Dictionary<T, int> _MultiSet;

		public MultiSet()
		{
			_MultiSet = new Dictionary<T, int>();
		}

	}
}

#endregion

