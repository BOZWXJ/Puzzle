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
		[STAThreadAttribute]
		static void Main(string[] args)
		{
			StringBuilder sb = new StringBuilder();
			Random rand = new Random();

			int N = 500;    //     500
			int M = 200000; // 20,0000
			int Q = 100000; // 10,0000
			sb.AppendLine(N + " " + M + " " + Q);
			for (int i = 0; i < M; i++) {
				int a = rand.Next(1, N + 1);
				int b = rand.Next(a, N + 1);
				sb.AppendLine(a + " " + b);
			}
			for (int i = 0; i < Q; i++) {
				int a = rand.Next(1, N + 1);
				int b = rand.Next(a, N + 1);
				sb.AppendLine(a + " " + b);
			}
			// 問題文出力
			string txt = sb.ToString();
			// Console.WriteLine(txt);
			File.WriteAllText(@"..\..\..\AtCoder\Problem.txt", txt, Encoding.ASCII);
		}
	}
}
