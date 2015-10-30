using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Q1
{
	class Program
	{
		static void Main(string[] args)
		{
			int count = 0;
			for (int gohyaku = 0; gohyaku * 500 < 1000; gohyaku++) {
				for (int hyaku = 0; hyaku * 100 < 1000; hyaku++) {
					for (int gojyuu = 0; gojyuu * 50 < 1000; gojyuu++) {
						for (int jyuu = 0; jyuu * 10 < 1000; jyuu++) {
							int total = gohyaku * 500 + hyaku * 100 + gojyuu * 50 + jyuu * 10;
							if (total == 1000 && gohyaku + hyaku + gojyuu + jyuu <= 15) {
								count++;
								Console.WriteLine(string.Format("{0,2}:500*{1,2}+100*{2,2}+50*{3,2}+10*{4,2}", count, gohyaku, hyaku, gojyuu, jyuu));
							}
						}
					}
				}
			}

			Console.Write("Hit Any Key");
			Console.Read();
		}
	}
}
