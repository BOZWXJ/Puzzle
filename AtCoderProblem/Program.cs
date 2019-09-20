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

			int N = 100;

			sb.AppendLine($"{N} 50");

			for (int i = 0; i < N; i++) {
				double tmp = rand.NextDouble();
				while (tmp == 0) {
					tmp = rand.NextDouble();
				}
				//sb.Append($"{tmp:0.##} ");
				sb.Append($"5 ");
			}
			sb.Remove(sb.Length-1, 1);
			sb.AppendLine();

			// 問題文出力
			string txt = sb.ToString();
			Console.WriteLine(txt);
			File.AppendAllText(@"..\..\..\AtCoder\Problem.txt", txt, Encoding.ASCII);
		}
	}
}
