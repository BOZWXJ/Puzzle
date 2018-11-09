using System;
using System.Collections.Generic;

namespace AtCoder
{
	public class Program
	{
		// ABC107-C ok
		static void Main(string[] args)
		{
			string[] vs = Console.ReadLine().Split();
			int N = int.Parse(vs[0]);
			int K = int.Parse(vs[1]);
			vs = Console.ReadLine().Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
			int[] pos = new int[N];
			for (int i = 0; i < N; i++) {
				pos[i] = int.Parse(vs[i]);
			}

			int p1 = 0, p2 = p1 + (K - 1);
			long sum = 0;
			for (int i = p1; i < p2; i++) {
				sum += Math.Abs(pos[i + 1] - pos[i]);
			}
			long time = 0;
			while (p2 < N) {
				long tmp = Math.Min(Math.Abs(pos[p1]), Math.Abs(pos[p2])) + sum;
				if (time == 0 || time > tmp) {
					time = tmp;
				}
				if (p1 + 1 < N) {
					sum -= Math.Abs(pos[p1] - pos[p1 + 1]);
				}
				if (p2 + 1 < N) {
					sum += Math.Abs(pos[p2] - pos[p2 + 1]);
				}
				p1++;
				p2++;
			}
			Console.WriteLine(time);
		}
	}
}

/*
5 3
-30 -10 10 20 50

40


3 2
10 20 30

20


1 1
0

0


8 5
-9 -7 -4 -3 1 2 3 4

10
*/
