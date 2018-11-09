using System;
using System.Collections.Generic;

namespace AtCoder
{
	public class Program
	{
		// ABC108-C ok
		static void Main(string[] args)
		{
			string[] sv = Console.ReadLine().Split();
			int N = int.Parse(sv[0]);
			int K = int.Parse(sv[1]);
			long c1 = 0, c2 = 0;
			for (int i = 1; i <= N; i++) {
				if (i % K == 0) {
					c1++;
					//System.Diagnostics.Debug.WriteLine(i);
				} else if (K % 2 == 0 && i % K == K / 2) {
					c2++;
					//System.Diagnostics.Debug.WriteLine(i);
				}
			}
			Console.WriteLine(c1 * c1 * c1 + c2 * c2 * c2);
		}
	}
}

/*
200000 2


35897 932
114191

31415 9265
27

5 3
1

3 2
9
(1,1,1),(1,1,3),
(1,3,1),(1,3,3),(2,2,2),(3,1,1),(3,1,3),
(3,3,1),(3,3,3)

200000 2
 */
