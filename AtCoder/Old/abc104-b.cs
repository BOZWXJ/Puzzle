using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder
{
	public class Program
	{
		// ABC104-B ok
		static void Main(string[] args)
		{
			string S = Console.ReadLine();
			// S の先頭の文字は大文字の A である。
			// S の先頭から 3 文字目と末尾から 2 文字目の間（両端含む）に大文字の C がちょうど 1 個含まれる。
			// 以上の A, C を除く S のすべての文字は小文字である。
			bool result = true;
			int c = 0;
			for (int i = 0; i < S.Length; i++) {
				if (i == 0) {
					if (S[i] != 'A') {
						result = false;
						break;
					}
				} else if ((2 <= i && i <= S.Length - 2) && S[i] == 'C') {
					c++;
				} else if (S[i] < 'a' || 'z' < S[i]) {
					result = false;
					break;
				}
			}
			if (c != 1) {
				result = false;
			}

			if (result) {
				Console.WriteLine("AC");
			} else {
				Console.WriteLine("WA");
			}
		}
	}
}
/*
Atcoder

AtCoCo

AcycliC

ACoder

AtCoder
 */
