﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RotationEditor
{
	public class MainViewModel : ViewModelBase
	{
		public ICommand TestingCommand { get; }
		public ICommand ExitCommand { get; }
		public ICommand SaveCommand { get; }
		public ICommand SaveAsCommand { get; }
		public ICommand OpenCommand { get; }
		public ICommand NewCommand { get; }
		public ICommand AboutCommand { get; }
		public ICommand RefreshCommand { get; }

		public ICommand ValidateCommand { get; }
		public ICommand DriftCorrectionCommand { get; }
		public ICommand NewPlateCommand { get; }
		public ICommand IndependentMoveCommand { get; }
		public ICommand JoinPlateCommand { get; }

		public ObservableCollection<RotationViewModel> Rotations { get; }

		public MainViewModel()
		{
			#region Normal commands
			ExitCommand = new ExitCommand();
			SaveCommand = new SaveCommand();
			SaveAsCommand = new SaveAsCommand();
			OpenCommand = new OpenCommand();
			NewCommand = new NewCommand();
			AboutCommand = new AboutCommand();
			RefreshCommand = new RefreshCommand();
			#endregion

			#region Special commands
			ValidateCommand = new ValidateCommand();
			DriftCorrectionCommand = new DriftCorrectionCommand();
			NewPlateCommand = new NewPlateCommand();
			IndependentMoveCommand = new IndependentMoveCommand();
			JoinPlateCommand = new JoinPlateCommand();
			#endregion

			TestingCommand = new TestingCommand();

			Rotations = new ObservableCollection<RotationViewModel>();

			#region Seeding rotations
			Rotations.Add(new RotationViewModel(100)
			{
				TimeStamp = 1500.0D,
				ConjugateID = 0,
				Comment = "Rotation 100",
			});
			Rotations.Add(new RotationViewModel(100)
			{
				TimeStamp = 1400.0D,
				ConjugateID = 0,
				Comment = "Rotation 100",
			});
			Rotations.Add(new RotationViewModel(100)
			{
				TimeStamp = 1300.0D,
				ConjugateID = 0,
				Comment = "Rotation 100",
			});
			Rotations.Add(new RotationViewModel(200)
			{
				TimeStamp = 1500.0D,
				ConjugateID = 0,
				Comment = "Rotation 200",
			});
			Rotations.Add(new RotationViewModel(200)
			{
				TimeStamp = 1400.0D,
				ConjugateID = 0,
				Comment = "Rotation 200",
			});
			#endregion
		}
	}
}