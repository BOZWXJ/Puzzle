using Livet;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PuzzleSolver.Models
{
	enum Direction { Up, Down, Right, Left }

	public class Model : NotificationObject
	{
		public ObservableCollection<string> SolverList { get; } = new ObservableCollection<string>();
		public ReactivePropertySlim<string> Solver { get; } = new ReactivePropertySlim<string>();
		public ReactivePropertySlim<string> ProblemText { get; } = new ReactivePropertySlim<string>();
		public ReactivePropertySlim<string> AnswerText { get; } = new ReactivePropertySlim<string>();
		public Model()
		{
			SolverList.Add("Sudoku");
			// カックロ
			SolverList.Add("Slide");
			SolverList.Add("Pluszle");
			// nQueen
			SolverList.Add("Frog");
			// Hanoi
			SolveBusy.Value = false;
		}

		public ReactiveProperty<bool> SolveBusy { get; } = new ReactiveProperty<bool>();
		Task<string> task;
		CancellationTokenSource cts = null;
		public async void Solve()
		{
			if (SolveBusy.Value) {
				return;
			}
			SolveBusy.Value = true;
			string problem = ProblemText.Value;
			string answer = AnswerText.Value;
			cts = new CancellationTokenSource();
			try {
				switch (Solver.Value) {
				case "Sudoku":
					task = Task.Run(() => Sudoku.Solver(ref problem, cts.Token), cts.Token);
					answer = await task;
					break;
				case "Slide":
					task = Task.Run(() => Slide.Solver(ref problem, cts.Token), cts.Token);
					answer = await task;
					break;
				case "Pluszle":
					answer = Pluszle.Solver(ref problem);
					break;
				case "Frog":
					answer = Frog.Solver(ref problem, cts.Token);
					break;
				}
			} catch (OperationCanceledException) {
				answer = "Cancel";
			} finally {
				cts.Dispose();
				cts = null;
			}
			ProblemText.Value = problem;
			AnswerText.Value = answer;
			SolveBusy.Value = false;
		}

		public void SolveCancel()
		{
			cts?.Cancel();
		}

	}
}
