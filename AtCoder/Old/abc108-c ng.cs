using System;
using System.Collections.Generic;

namespace AtCoder
{
	public class Program
	{
		// ABC108-C ng
		static void Main(string[] args)
		{
			string[] sv = Console.ReadLine().Split();
			int N = int.Parse(sv[0]);
			int K = int.Parse(sv[1]);
			int count = 0;
			for (int x = K; x <= 2 * N; x += K) {
				//System.Diagnostics.Debug.WriteLine(string.Format("{0} <= {1} = 2 * {2}", x, 2 * N, N));
				for (int a = 1; a <= N; a++) {
					int b = x - a;
					if (0 < b && b <= N) {
						if (a - b == 0 || (a - b) % K == 0) {
							for (int c = K - Math.Min(a, b); c <= N; c += K) {
								if (c > 0) {
									count++;
									//System.Diagnostics.Debug.Write(string.Format("({0},{1},{2}) ", a, b, c));
								}
							}
						}
					}
				}
				//System.Diagnostics.Debug.WriteLine("");
			}
			Console.WriteLine(count);
		}
	}
}
