using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtCoderProblem
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			StringBuilder sb = new StringBuilder();
			Random rand = new Random();

			int N = 2;
			int Q = (int)PowerOf10(5);

			sb.AppendLine($"{N} {Q}");

			for (int i = 1; i < N; i++) {
				sb.AppendLine($"{i} {i + 1}");
			}

			for (int i = 0; i < Q; i++) {
				sb.AppendLine($"1 {2 * PowerOf10(5)}");
			}

			sb.AppendLine();

			// 問題文出力
			string txt = sb.ToString();
			Console.WriteLine(txt);
			File.AppendAllText(@"..\..\..\AtCoder\Problem.txt", txt, Encoding.ASCII);
		}

		static long PowerOf10(int x)
		{
			return (long)Math.Pow(10, x);
		}

	}
}
