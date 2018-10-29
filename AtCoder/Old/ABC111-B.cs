using System;

namespace AtCoder
{
	public class Program
	{
		// ABC111-B OK
		static void Main(string[] args)
		{
			int n = int.Parse(Console.ReadLine());
			int x = 111;
			while (n > x) {
				x += 111;
			}
			Console.WriteLine(x);
			System.Diagnostics.Debug.WriteLine(x);
		}
	}
}
