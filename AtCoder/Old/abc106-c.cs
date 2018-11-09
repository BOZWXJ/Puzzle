using System;
using System.Collections.Generic;

namespace AtCoder
{
	public class Program
	{
		// ABC106-C ok
		static void Main(string[] args)
		{
			string S = Console.ReadLine();
			long K = long.Parse(Console.ReadLine());

			int count = 0;
			for (int i = 0; i < S.Length; i++) {
				if (S[i] == '1') {
					count++;
					if (count == K) {
						Console.WriteLine(S[i]);
						break;
					}
				} else {
					Console.WriteLine(S[i]);
					break;
				}
			}
		}
	}
}
