using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RotationEditor.ViewModel
{
	public class CratonCreationViewModel : ViewModelBase
	{
		private int numberOfCratons;
		private int minCratonID;
		private int cratonIDIncrement;
		private double startTime;

		public ObservableCollection<CratonViewModel> Cratons { get; set; }

		public int NumberOfCratons
		{
			get => numberOfCratons;
			set
			{
				if(numberOfCratons != value)
				{
					numberOfCratons = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(IsDataValid));
				}
			}
		}
		public int MinCratonID
		{
			get => minCratonID;
			set
			{
				if(minCratonID != value)
				{
					minCratonID = value;
					OnPropertyChanged();
				}
			}
		}
		public int CratonIDIncrement
		{
			get => cratonIDIncrement;
			set
			{
				if(cratonIDIncrement != value)
				{
					cratonIDIncrement = value;
					OnPropertyChanged();
				}
			}
		}

		public double StartTime
		{
			get => startTime;
			set
			{
				if(startTime != value)
				{
					startTime = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(IsDataValid));
				}
			}
		}

		public bool IsDataValid => Cratons.Count > 0;

		public ICommand GenerateCratonsCommand { get; set; }
		public ICommand ClearCratonsCommand { get; set; }

		public CratonCreationViewModel()
		{
			Cratons = new ObservableCollection<CratonViewModel>();
			GenerateCratonsCommand = new RelayCommand(GenerateCratons);
			ClearCratonsCommand = new RelayCommand(() =>
			{
				Cratons.Clear();
				OnPropertyChanged(nameof(Cratons));
				OnPropertyChanged(nameof(IsDataValid));
			});

			CratonIDIncrement = 1;
			MinCratonID = 1;
		}

		internal void GenerateCratons()
		{
			for(int i = 0; i < NumberOfCratons; i++)
			{
				Cratons.Add(new CratonViewModel(MinCratonID + i * CratonIDIncrement, $"Craton {i + 1}")); // TODO: have naming scheme implemented
			}

			OnPropertyChanged(nameof(Cratons));
			OnPropertyChanged(nameof(IsDataValid));
		}
	}

	public class CratonViewModel
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public CratonViewModel(int id, string name) 
		{
			ID = id;
			Name = name;
		}
	}
}
