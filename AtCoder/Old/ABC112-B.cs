using System;

namespace AtCoder
{
	class Program
	{
		// ABC112-B OK
		static void Main(string[] args)
		{
			string[] s = Console.ReadLine().Split();
			int N = int.Parse(s[0]);
			int T = int.Parse(s[1]);
			int ans = int.MaxValue;
			for (int i = 0; i < N; i++) {
				string[] vs = Console.ReadLine().Split();
				int c = int.Parse(vs[0]);
				int t = int.Parse(vs[1]);
				if (t <= T && c < ans) {
					ans = c;
				}
			}
			if (ans < int.MaxValue) {
				Console.WriteLine(ans);
			} else {
				Console.WriteLine("TLE");
			}
		}
	}
}
