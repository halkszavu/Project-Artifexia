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
			});
		}

		internal void GenerateCratons()
		{
			for(int i = 0; i < NumberOfCratons; i++)
			{
				Cratons.Add(new CratonViewModel(MinCratonID + i * CratonIDIncrement, $"Craton {i + 1}")); // Todo: have naming scheme implemented
			}

			OnPropertyChanged(nameof(Cratons));
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
