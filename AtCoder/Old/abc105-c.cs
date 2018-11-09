using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder
{
	public class Program
	{
		// ABC105-C ok
		static void Main(string[] args)
		{
			int N = int.Parse(Console.ReadLine());
			long posi = 0, nega = 0, diff = N;

			long[] pattern = new long[64];
			long m = 1;
			for (long i = 0; i < pattern.Length; i++) {
				pattern[i] = m;
				m = (m << 1) | i % 2;
			}

			while (true) {
				if (diff == 0) {
					// 終了
					//Console.WriteLine(posi + (-1 * nega));
					Console.WriteLine(Convert.ToString(posi + nega, 2));
					return;
				} else if (diff > 0) {
					// 正
					int i = 0;
					while (pattern[i] < diff) {
						i += 2;
					}
					posi += 1L << i;
					//Console.WriteLine(i);
				} else {
					// 負
					int i = 1;
					while (-1 * pattern[i] > diff) {
						i += 2;
					}
					nega += 1L << i;
					//Console.WriteLine(i);
				}
				diff = N - (posi + (-1 * nega));
				//Console.WriteLine(posi + (-1 * nega));
				//Console.WriteLine(Convert.ToString(posi + nega, 2));
				//Console.WriteLine(diff);
			}
		}
	}
}
/*
-1000000000
11000101101001010100101000000000

1000000000
1001100111011111101111000000000

1
1

0
0

123456789
11000101011001101110100010101

-9
1011
 */
