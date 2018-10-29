using System;
using System.Collections.Generic;

namespace AtCoder
{
	public class Program
	{
		// ABC111-C OK
		static void Main(string[] args)
		{
			Dictionary<string, int> odd = new Dictionary<string, int>();
			Dictionary<string, int> even = new Dictionary<string, int>();

			int n = int.Parse(Console.ReadLine());
			string[] vs = Console.ReadLine().Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < n; i++) {
				if (i % 2 != 0) {
					if (!odd.ContainsKey(vs[i])) {
						odd[vs[i]] = 0;
					}
					odd[vs[i]]++;
				} else {
					if (!even.ContainsKey(vs[i])) {
						even[vs[i]] = 0;
					}
					even[vs[i]]++;
				}
			}

			string odd1Str = "", odd2Str = "", even1Str = "", even2Str = "";
			int odd1Cnt = 0, odd2Cnt = 0, even1Cnt = 0, even2Cnt = 0;
			FindMax(odd, ref odd1Str, ref odd1Cnt, ref odd2Str, ref odd2Cnt);
			FindMax(even, ref even1Str, ref even1Cnt, ref even2Str, ref even2Cnt);

			int result = 0;
			if (odd1Str != even1Str) {
				// 奇数の最頻値と偶数の最頻値が違う
				result = n - odd1Cnt - even1Cnt;
			} else {
				// 奇数の最頻値と偶数の最頻値が同じ
				if (odd1Cnt + even2Cnt > even1Cnt + odd2Cnt) {
					result = n - odd1Cnt - even2Cnt;
				} else {
					result = n - even1Cnt - odd2Cnt;
				}
			}

			foreach (var s in odd) {
				System.Diagnostics.Debug.Write(s + " ");
			}
			System.Diagnostics.Debug.WriteLine("");
			foreach (var s in even) {
				System.Diagnostics.Debug.Write(s + " ");
			}
			System.Diagnostics.Debug.WriteLine("");
			System.Diagnostics.Debug.WriteLine(string.Format("[{0}, {1}] [{2}, {3}]", odd1Str, odd1Cnt, odd2Str, odd2Cnt));
			System.Diagnostics.Debug.WriteLine(string.Format("[{0}, {1}] [{2}, {3}]", even1Str, even1Cnt, even2Str, even2Cnt));
			System.Diagnostics.Debug.WriteLine(result);

			Console.WriteLine(result);
		}

		private static void FindMax(Dictionary<string, int> dic, ref string max1Str, ref int max1Cnt, ref string max2Str, ref int max2Cnt)
		{
			foreach (var item in dic) {
				if (item.Value > max1Cnt) {
					max2Str = max1Str;
					max2Cnt = max1Cnt;
					max1Str = item.Key;
					max1Cnt = item.Value;
				} else if (item.Value > max2Cnt) {
					max2Str = item.Key;
					max2Cnt = item.Value;
				}
			}
		}
	}
}
