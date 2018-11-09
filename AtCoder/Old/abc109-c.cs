using System;
using System.Collections.Generic;

namespace AtCoder
{
	public class Program
	{
		// ABC109-C ok
		static void Main(string[] args)
		{
			string[] vs = Console.ReadLine().Split();
			int N = int.Parse(vs[0]);   // 個数
			int X = int.Parse(vs[1]);   // 開始位置
			int[] d = new int[N];       // 各都市絶対距離
			int dMin = int.MaxValue;
			vs = Console.ReadLine().Split();
			for (int i = 0; i < N; i++) {
				d[i] = Math.Abs(int.Parse(vs[i]) - X);
				dMin = Math.Min(dMin, d[i]);
			}
			for (int i = dMin; i > 0; i--) {
				bool f = true;
				foreach (var j in d) {
					if (j % i !=0) {
						f = false;
						break;
					}
				}
				if (f) {
					Console.WriteLine(i);
					return;
				}
			}
		}
	}
}
