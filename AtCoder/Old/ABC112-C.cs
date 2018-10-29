using System;

namespace AtCoder
{
	public class Program
	{
		// ABC112-C OK
		static void Main(string[] args)
		{
			int n = int.Parse(Console.ReadLine());
			int[] x1 = new int[100];
			int[] y1 = new int[100];
			int[] h1 = new int[100];
			int n1 = 0;
			int[] x0 = new int[100];
			int[] y0 = new int[100];
			int n0 = 0;
			for (int i = 0; i < n; i++) {
				string[] s = Console.ReadLine().Split();
				int x = int.Parse(s[0]);
				int y = int.Parse(s[1]);
				int h = int.Parse(s[2]);
				if (h > 0) {
					x1[n1] = x;
					y1[n1] = y;
					h1[n1] = h;
					n1++;
				} else {
					x0[n0] = x;
					y0[n0] = y;
					n0++;
				}
			}
			if (n1 == 1) {
				Console.WriteLine(string.Format("{0} {1} {2}", x1[0], y1[0], h1[0]));
				return;
			}

			for (int Cy = 0; Cy <= 100; Cy++) {
				for (int Cx = 0; Cx <= 100; Cx++) {
					bool f = true;
					int Ch = 0;
					Ch = h1[0] + Math.Abs(x1[0] - Cx) + Math.Abs(y1[0] - Cy);
					for (int i = 1; i < n1; i++) {
						if (Ch != h1[i] + Math.Abs(x1[i] - Cx) + Math.Abs(y1[i] - Cy)) {
							f = false;
							break;
						}
					}
					if (f) {
						for (int i = 0; i < n0; i++) {
							if (Ch - Math.Abs(x0[i] - Cx) - Math.Abs(y0[i] - Cy) > 0) {
								f = false;
								break;
							}
						}
						if (f) {
							Console.WriteLine(string.Format("{0} {1} {2}", Cx, Cy, Ch));
							return;
						}
					}
				}
			}
		}
	}
}