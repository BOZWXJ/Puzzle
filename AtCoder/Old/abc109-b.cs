using System;
using System.Collections.Generic;

namespace AtCoder
{
	public class Program
	{
		// ABC109-B ok
		static void Main(string[] args)
		{
			string result = "Yes";
			HashSet<string> dic = new HashSet<string>();
			char lastChar = ' ';
			int n = int.Parse(Console.ReadLine());
			for (int i = 0; i < n; i++) {
				string word = Console.ReadLine();
				if (i == 0 || (!dic.Contains(word) && word[0] == lastChar)) {
					dic.Add(word);
					lastChar = word[word.Length - 1];
				} else {
					result = "No";
					break;
				}
			}
			Console.WriteLine(result);
		}
	}
}
