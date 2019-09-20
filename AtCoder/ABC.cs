using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AtCoder
{
	public class Program
	{
		static long mod = 1000000007;

		static void Main(string[] args)
		{
			int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			int N = vs[0];
			int M = vs[1];
			int[] a = Console.ReadLine().Split().Select(int.Parse).ToArray();

			PriorityQueue<int> queue = new AtCoder.PriorityQueue<int>(true);
			foreach (int item in a) {
				queue.Enqueue(item);
			}
			//System.Diagnostics.Debug.WriteLine(queue);

			for (int i = 0; i < M; i++) {
				int item = queue.Dequeue();
				item /= 2;
				queue.Enqueue(item);
			}
			//System.Diagnostics.Debug.WriteLine(queue);

			long ans = 0;
			foreach (var item in queue) {
				ans += item;
			}
			Console.WriteLine(ans);

		}
	}

	#region PriorityQueue

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
				_Compare = (T x, T y) => Comparer<T>.Default.Compare(x, y);
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
			var ret = _Heap[0];
			int size = _Heap.Count - 1;
			var x = _Heap[size];
			var i = 0;
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

	#endregion

}
