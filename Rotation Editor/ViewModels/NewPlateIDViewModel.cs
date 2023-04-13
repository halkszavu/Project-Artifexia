using RotationEditor.Commands;
using RotationModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RotationEditor.ViewModel
{
	public class NewPlateIDViewModel : ViewModelBase
	{
		public ObservableCollection<int> PlateIDs { get; }

		int _newPlate;
		public int NewPlate
		{
			get => _newPlate;
			set
			{
				if (_newPlate != value)
				{
					_newPlate = value;
					OnPropertyChanged();
				}
			}
		}
		int _select;
		public int SelectedPlateIndex
		{
			get => _select;
			set
			{
				if (value != _select)
				{
					_select = value;
					OnPropertyChanged();
				}
			}
		}

		public int SelectedPlateId => PlateIDs[SelectedPlateIndex];

		public ICommand GenerateNewPlateID { get; }

		public NewPlateIDViewModel(IGetPlateIDsService plateIDsService) : base()
		{
			PlateIDs = new ObservableCollection<int>(plateIDsService.GetPlateIDs);
			GenerateNewPlateID = new GenerateNewPlateIDCommand(this);
		}
	}
}