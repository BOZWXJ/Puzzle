﻿using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PuzzleSolver.Models
{
	internal class Frog
	{
		static int height;
		static int width;
		static int[,] board;
		static int[,] answer;
		static (int row, int col) start;
		static (int row, int col) goal;
		static int goalStep;

		internal static string Solver(ref string problem, CancellationToken token)
		{
			if (string.IsNullOrWhiteSpace(problem)) {
				problem = "xxxxxxxx\r\nx331111x\r\nx131111x\r\nx113111G\r\nS111211x\r\nx111111x\r\nx111111x\r\nxxxxxxxx\r\n";
			}

			// 問題文
			try {
				string[] lines = problem.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
				height = lines.Length;
				width = lines.Select(p => p.Length).Max();
				board = new int[height, width];
				answer = new int[height, width];
				for (int i = 0; i < height; i++) {
					for (int j = 0; j < width; j++) {
						board[i, j] = -1;
						answer[i, j] = -1;
					}
				}
				start = default;
				goal = default;
				goalStep = 0;
				foreach (var (s, i) in lines.Select((p, i) => (p, i))) {
					foreach (var (c, j) in s.Select((p, i) => (p, i))) {
						if ('1' <= c && c <= '9') {
							board[i, j] = c - 0x30;
							answer[i, j] = 0;
							goalStep++;
						} else if (c == 's' || c == 'S') {
							if (start != default) {
								return "Error";
							}
							start = (i, j);
							board[i, j] = 1;
							answer[i, j] = 1;
						} else if (c == 'g' || c == 'G') {
							if (goal != default) {
								return "Error";
							}
							goal = (i, j);
							board[i, j] = 1;
							answer[i, j] = 0;
						}
					}
				}
				if (start == default || goal == default) {
					return "Error";
				}
			} catch {
				return "Error";
			}

			// 探索
			if (Saiki(start.row, start.col, 1, token)) {
				// 結果
				return PrintBoard(answer);
			}
			return "No";
		}

		static readonly (int, int)[] pos = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
		private static bool Saiki(int row, int col, int cnt, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			if (CheckComplete(row, col, cnt)) {
				return true;
			}
			foreach (var (i, j) in pos) {
				int r = row + board[row, col] * i;
				int c = col + board[row, col] * j;
				if (0 <= r && r < height && 0 <= c && c < width && board[r, c] > 0 && answer[r, c] == 0) {
					answer[r, c] = cnt;
					//System.Diagnostics.Debug.WriteLine($"\"({row},{col})\"->\"({r},{c})\"");
					if (Saiki(r, c, cnt + 1, token)) {
						return true;
					}
					answer[r, c] = 0;
				}
			}
			return false;
		}

		// 完了
		private static bool CheckComplete(int row, int col, int cnt)
		{
			if ((row, col) == goal && cnt == goalStep + 2) {
				return true;
			}
			return false;
		}

		static readonly StringBuilder sb = new();
		private static string PrintBoard(int[,] data)
		{
			sb.Clear();
			int l = 0;
			for (int i = 0; i < data.GetLength(0); i++) {
				for (int j = 0; j < data.GetLength(1); j++) {
					if (data[i, j] > 0) {
						l = Math.Max(l, data[i, j].ToString().Length);
					}
				}
			}
			for (int i = 0; i < data.GetLength(0); i++) {
				sb.Append("");
				for (int j = 0; j < data.GetLength(1); j++) {
					if (l > 1 && j > 0) {
						sb.Append(",");
					}
					if ((i, j) == start) {
						sb.Append("S".PadLeft(l));
					} else if ((i, j) == goal) {
						sb.Append("G".PadLeft(l));
					} else if (data[i, j] < 0) {
						sb.Append("*".PadLeft(l));
					} else {
						sb.Append($"{data[i, j]}".PadLeft(l));
					}
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

	}
}
