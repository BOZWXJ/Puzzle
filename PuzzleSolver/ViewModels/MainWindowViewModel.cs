using Livet;
using Livet.Commands;
using Livet.EventListeners;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.Messaging.Windows;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using PuzzleSolver.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Reactive.Linq;

namespace PuzzleSolver.ViewModels
{
	public class MainWindowViewModel : ViewModel
	{
		private Model model;
		public ReactivePropertySlim<string> ProblemText { get; }
		public ReactivePropertySlim<string> AnswerText { get; }
		public ReadOnlyReactiveCollection<string> SolverList { get; }
		public ReactivePropertySlim<string> Solver { get; }
		public ReadOnlyReactivePropertySlim<Visibility> SolveBusy { get; }
		public ReactiveCommand Solve { get; } = new ReactiveCommand();
		// Some useful code snippets for ViewModel are defined as l*(llcom, llcomn, lvcomm, lsprop, etc...).
		public MainWindowViewModel()
		{
			model = new Model();
			ProblemText = model.ToReactivePropertySlimAsSynchronized(p => p.ProblemText.Value);
			AnswerText = model.ToReactivePropertySlimAsSynchronized(p => p.AnswerText.Value);
			SolverList = model.SolverList.ToReadOnlyReactiveCollection(p => p);
			Solver = model.ToReactivePropertySlimAsSynchronized(p => p.Solver.Value);
			SolveBusy = model.SolveBusy.Select(p => p ? Visibility.Visible : Visibility.Hidden).ToReadOnlyReactivePropertySlim();
			Solve.Subscribe(() => {
				if (!model.SolveBusy.Value) {
					model.Solve();
				} else {
					model.SolveCancel();
				}
			});
		}

		public void Initialize()
		{

		}
	}
}
